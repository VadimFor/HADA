using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using library.EN;
using System.Data.SqlClient;

namespace library.CAD
{
    public class CAD_Venta : CAD_Anuncio
    {
        /// <summary>
        /// Crea un nuevo anuncio de venta en la BD con los datos del anuncio representado por el parámetro en.
        /// </summary>
        /// <param name="en">Datos de la venta.</param>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public bool createVenta(EN_Venta en)
        {
            base.createAnuncio(en);
            bool created = false;
            try
            {
                c.Open();
                SqlCommand comm = new SqlCommand("SELECT IDENT_CURRENT ('anuncio')", c);
                en.Id = Convert.ToInt32(comm.ExecuteScalar());
                string comando = "update anuncio set precio_por_m = " + en.PrecioPorM + " where id = " + en.Id;
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
        /// Devuelve el anuncio de venta indicado leído de la BD.
        /// </summary>
        /// <param name="en">Datos de la venta.</param>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public bool readVenta(EN_Venta en)
        {
            base.readAnuncio(en);
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
                    en.PrecioPorM = float.Parse(dr["fianza"].ToString());
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
            if (en.Id == -1)
            {
                return false;
            }
            return read;
        }
        /// <summary>
        /// Actualiza los datos de un anuncio de venta en la BD con los datos del anuncio representado por el parámetro en.
        /// </summary>
        /// <param name="en">Datos de la venta.</param>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public bool updateVenta(EN_Venta en)
        {
            base.updateAnuncio(en);
            bool updated = false;
            string comando = "Update Anuncio set precio = " + en.Precio + ", desc = '" + en.Desc + "', propiedad = " + en.Propiedad + ", contacto = '" + en.Contacto + "', fianza = null, precio_por_m = " + en.PrecioPorM + " where id = " + en.Id;
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
    }
}
