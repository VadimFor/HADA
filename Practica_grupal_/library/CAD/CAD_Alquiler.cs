using library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace library.CAD
{
    public class CAD_Alquiler : CAD_Anuncio
    {
        /// <summary>
        /// Crea un nuevo anuncio de alquiler en la BD con los datos del anuncio representado por el parámetro en.
        /// </summary>
        /// <param name="en">Datos del alquiler.</param>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public bool createAlquiler(EN_Alquiler en)
        {
            base.createAnuncio(en);
            bool created = false;
            try
            {
                c.Open();
                SqlCommand comm = new SqlCommand("SELECT IDENT_CURRENT ('anuncio')", c);
                en.Id = Convert.ToInt32(comm.ExecuteScalar());
                string comando = "update anuncio set fianza = " + en.Fianza + " where id = " + en.Id;
                comm = new SqlCommand(comando, c);
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
        /// Devuelve el anuncio de alquiler indicado leído de la BD.
        /// </summary>
        /// <param name="en">Datos del alquiler.</param>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public bool readAlquiler(EN_Alquiler en)
        {
            bool read = false;
            try
            {
                c.Open();
                string comando = "Select * from Anuncio where id = " + en.Id;
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
                    en.Fianza = float.Parse(dr["fianza"].ToString());
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
            if(en.Id == -1)
            {
                return false;
            }
            return read;
        }
        /// <summary>
        /// Actualiza los datos de un anuncio de alquiler en la BBDD con los datos del anuncio representado por el parámetro en.
        /// </summary>
        /// <param name="en">Datos del alquiler.</param>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public bool updateAlquiler(EN_Alquiler en)
        {
            base.updateAnuncio(en);
            bool updated = false;
            string comando = "Update Anuncio set precio = " + en.Precio + ", desc = '" + en.Desc + "', propiedad = " + en.Propiedad + ", contacto = " + en.Contacto + ", fianza = " + en.Fianza + ", precio_por_m = null where id = " + en.Id;
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
        /// Borra el anuncio de alquiler representado en en de la BD.
        /// </summary>
        /// <param name="en">Datos del alquiler.</param>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public bool deleteAlquiler(EN_Alquiler en)
        {
            return base.deleteAnuncio(en);
        }
    }
}