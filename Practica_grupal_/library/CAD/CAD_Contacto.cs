using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.EN;

namespace library.CAD
{
    public class CAD_Contacto
    {
        private string constring { get; set; }

        public CAD_Contacto()
        {
            constring = ConfigurationManager.ConnectionStrings["InmoData"].ToString();
        }

        public bool crearContacto(EN_Contacto en)
        {
            bool creado = false;
            SqlConnection conn = null;
            string comandoCrear = "Insert into Contacto(correo, nombre, apellido, telefono) VALUES ('" + en.Correo + "','" + en.Nombre + "','" + en.Apellido + "','" + en.Telefono + "')";

            try
            {
                conn = new SqlConnection(constring);
                conn.Open();

                if (!en.leerContacto())
                {
                    SqlCommand insert = new SqlCommand(comandoCrear, conn);
                    insert.ExecuteNonQuery();
                    creado = true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("User operation has failed.Error: { 0}", ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return creado;
        }

        public bool leerContacto(EN_Contacto en)
        {
            bool leido = false;
            SqlConnection conn = null;
            string comandoSelect = "Select * from Contacto where correo = '" + en.Correo + "'";

            try
            {
                conn = new SqlConnection(constring);
                conn.Open();

                SqlCommand read = new SqlCommand(comandoSelect, conn);
                SqlDataReader lector = read.ExecuteReader();

                if (lector.Read())
                {
                    en.Nombre = lector["nombre"].ToString();
                    en.Apellido = lector["apellido"].ToString();
                    en.Telefono = lector["telefono"].ToString();
                    //en.Foto = lector["foto"].ToString();
                    leido = true;
                }
                lector.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("User operation has failed.Error: { 0}", ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return leido;
        }

        public bool actualizarContacto(EN_Contacto en)
        {
            bool actualizado = false;
            SqlConnection conn = null;
            string comandoActualizar = "Update Contacto set nombre ='" + en.Nombre + "',apellido ='" + en.Apellido + "',telefono ='" + en.Telefono + "' where correo ='" + en.Correo + "'";

            try
            {
                conn = new SqlConnection(constring);
                conn.Open();

                if (en.leerContacto())
                {
                    SqlCommand update = new SqlCommand(comandoActualizar, conn);
                    update.ExecuteNonQuery();
                    actualizado = true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("User operation has failed.Error: { 0}", ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return actualizado;
        }

        public bool eliminarContacto(EN_Contacto en)
        {
            bool eliminado = false;
            SqlConnection conn = null;
            string comandoBorrar = "Delete from Contacto where correo = '" + en.Correo + "'";

            try
            {
                conn = new SqlConnection(constring);
                conn.Open();

                if (en.leerContacto())
                {
                    SqlCommand delete = new SqlCommand(comandoBorrar, conn);
                    delete.ExecuteNonQuery();
                    eliminado = true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("User operation has failed.Error: { 0}", ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null) conn.Close();
            }

            return eliminado;
        }

    }
}
