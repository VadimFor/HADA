using library.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.CAD
{
    public class CAD_Propiedad
    {
        private string constring;

        public CAD_Propiedad()
        {
            constring = ConfigurationManager.ConnectionStrings["InmoData"].ConnectionString;
        }


        public DataTable mostrarPropiedades()
        {
            DataTable lista = new DataTable();
            SqlConnection conn = null;
            
            try
            {
                conn = new SqlConnection(constring);
                conn.Open();

                SqlDataAdapter show = new SqlDataAdapter("Select p.*, a.precio from Anuncio a, Propiedad p where p.id = a.propiedad", conn);
                show.Fill(lista);
            }
            catch (Exception ex)
            {
                Console.WriteLine("User operation has failed.Error: {0},", ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista;
        }

        public DataTable filtrarCategorias(string categoria)
        {
            DataTable lista = new DataTable();
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(constring);
                conn.Open();

                SqlDataAdapter show = new SqlDataAdapter("Select p.*, a.precio from Anuncio a, Propiedad p where p.id = a.propiedad and p.categoria = '" + categoria + "'", conn);
                show.Fill(lista);
            }
            catch (Exception ex)
            {
                Console.WriteLine("User operation has failed.Error: {0},", ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista;
        }

        public bool nuevaPropiedad(EN_Propiedad en)
        {
            bool creado = false;
            SqlConnection conn = null;
            string comandoCrear = "Insert into Propiedad(metros, cod_postal, usuario, categoria, direccion, img1, img2, img3) VALUES ('" + en.Metros + "','" + en.Cod_postal + "','" + en.Usuario + "','" + en.Categoria + "','" + en.Direccion + "','" + en.Img1 + "','" + en.Img2 + "','" + en.Img3 + "')";

            try
            {
                conn = new SqlConnection(constring);
                conn.Open();

                if (!en.leerPropiedad())
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

        public bool leerPropiedad(EN_Propiedad en)
        {
            bool leido = false;

            SqlConnection conn = null;
            string comandoSelect = "Select * from Propiedad where id = '" + en.Id + "'";

            try
            {
                conn = new SqlConnection(constring);
                conn.Open();

                SqlCommand read = new SqlCommand(comandoSelect, conn);
                SqlDataReader lector = read.ExecuteReader();

                if (lector.Read())
                {
                    en.Metros = int.Parse(lector["metros"].ToString());
                    en.Cod_postal = lector["cod_postal"].ToString();
                    en.Usuario = lector["usuario"].ToString();
                    en.Categoria = lector["categoria"].ToString();
                    en.Img1 = lector["imagen"].ToString();
                    en.Img2 = lector["imagen"].ToString();
                    en.Img3 = lector["imagen"].ToString();
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

        public bool actualizarPropiedad(EN_Propiedad en)
        {
            bool actualizado = false;
            SqlConnection conn = null;
            string comandoActualizar = "Update Propiedad set metros ='" + en.Metros + "',cod_postal ='" + en.Cod_postal + "',usuario ='" + en.Usuario + "',categoria ='" + en.Categoria + "' where id ='" + en.Id + "'";
            
            try
            {
                conn = new SqlConnection(constring);
                conn.Open();

                if (en.leerPropiedad())
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

        public bool eliminarPropiedad(EN_Propiedad en)
        {
            bool eliminado = false;
            SqlConnection conn = null;
            string comandoBorrar = "Delete from Propiedad where id = '" + en.Id + "'";

            try
            {
                conn = new SqlConnection(constring);
                conn.Open();

                if (en.leerPropiedad())
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

        public DataSet admin_getPropiedades()
        {
            DataSet dataset = new DataSet();
            string query = "Select * from [dbo].[Propiedad]";

            try
            {
                //OBTENGO LOS DATOS DE LA BASE DE DATOS
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InmoData"].ToString());
                SqlDataAdapter sqladaprter = new SqlDataAdapter(query, con);
                sqladaprter.Fill(dataset, "Propiedad");

            }
            catch (SqlException e) { Console.WriteLine($"Exception de CAD_Propiedad: {e.Message}"); }
            catch (Exception e) { Console.WriteLine($"Exception de CAD_Propiedad: {e.Message}"); }

            return dataset;
        }

        public bool admin_modPropiedades(DataSet dataset, SqlCommand updateCommand)
        {
            bool modificados = false;
            try
            {
                //OBTENGO LOS DATOS DE LA BASE DE DATOS
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InmoData"].ToString());
                string query = "Select * from [dbo].[Propiedad]";
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, con);

                ////// UPDATE
                string updateQuery = "Update [dbo].[Propiedad] set metros = @metros," +
                                                         "cod_postal = @cod_postal," +
                                                         "usuario = @usuario," +
                                                         "categoria = @categoria," +
                                                         "direccion = @direccion," +
                                                         "img1 = @img1," +
                                                         "img2 = @img2," +
                                                         "img3 = @img3" +
                                                         " where id = @id";

                updateCommand.CommandText = updateQuery;
                updateCommand.Connection = con;

                sqlAdapter.UpdateCommand = updateCommand;

                sqlAdapter.Update(dataset, "Propiedad");

                modificados = true;
            }
            catch (SqlException e) { Console.WriteLine($"Exception de CAD_Propiedad: {e.Message}"); }
            catch (Exception e) { Console.WriteLine($"Exception de CAD_Propiedad: {e.Message}"); }

            return modificados;
        }

        public bool admin_deletePropiedades(DataSet dataset, SqlCommand deleteCommand)
        {
            bool borrado = false;
            try
            {
                //OBTENGO LOS DATOS DE LA BASE DE DATOS
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InmoData"].ToString());
                string query = "Select * from [dbo].[Propiedad]";
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, con);

                string deletequery = "Delete from [dbo].[Propiedad] where id = @id";

                deleteCommand.CommandText = deletequery;
                deleteCommand.Connection = con;

                sqlAdapter.DeleteCommand = deleteCommand;

                sqlAdapter.Update(dataset, "Propiedad");

                borrado = true;

            }
            catch (SqlException e) { Console.WriteLine($"Exception de CAD_Propiedad: {e.Message}"); }
            catch (Exception e) { Console.WriteLine($"Exception de CAD_Propiedad: {e.Message}"); }

            return borrado;
        }

        public int obtenerId(EN_Propiedad en) 
        {
            int idPublicar = -1;
            SqlConnection conn = null;
            string comandoSelect = "Select id from Propiedad order by id desc";

            try
            {
                conn = new SqlConnection(constring);
                conn.Open();

                SqlCommand read = new SqlCommand(comandoSelect, conn);
                SqlDataReader lector = read.ExecuteReader();

                if (lector.Read())
                {
                    idPublicar = int.Parse(lector["id"].ToString());
                }
                lector.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("User operation has failed.Error: { 0}", ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return idPublicar;
        }

        public DataTable mostrarPropiedadesDefault()
        {
            DataTable lista = new DataTable();
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(constring);
                conn.Open();

                SqlDataAdapter show = new SqlDataAdapter("Select top 3 p.*, a.precio, a.id anuncioId from Anuncio a, Propiedad p where p.id = a.propiedad", conn);
                show.Fill(lista);
            }
            catch (Exception ex)
            {
                Console.WriteLine("User operation has failed.Error: {0},", ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return lista;
        }
    }
}
