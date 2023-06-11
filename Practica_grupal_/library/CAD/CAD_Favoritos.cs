using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.EN;
using library.CAD;

namespace library.CAD
{
    /// <summary>
    /// Clase CAD_Favoritos.
    /// </summary>
    class CAD_Favoritos
    {
        private string constring;

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        public CAD_Favoritos()
        {
            constring = ConfigurationManager.ConnectionStrings["InmoData"].ToString();
        }

        /// <summary>
        /// Metodo readFavoritos que lee un favorito.
        /// </summary>
        /// <param name="en">Parametro del tipo EN_Favoritos.</param>
        /// <returns>Devuelve true si se ha leido de forma correcta.</returns>
        public bool readFavoritos(EN_Favoritos en)
        {
            bool leido = false;
            string query = "";
            if (en.volver)
            {
                query = "select top " + en.limite + "a.precio, a.id, a.\"desc\", p.metros,p.cod_postal, p.categoria, p.img1 from Favorito f inner join Anuncio a on f.anuncio = a.id inner join Propiedad p on a.propiedad = p.id where f.usuario = '" + en.usuario.email + "' and f.anuncio < " + en.desde + " order by f.anuncio desc;";
            }
            else
            {
                query = "select top " + en.limite + "a.precio, a.id, a.\"desc\", p.metros,p.cod_postal, p.categoria, p.img1 from Favorito f inner join Anuncio a on f.anuncio = a.id inner join Propiedad p on a.propiedad = p.id where f.usuario = '" + en.usuario.email + "' and f.anuncio > " + en.hasta + " order by f.anuncio;";
            }


            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand(query, connection);
                    SqlDataReader dr = com.ExecuteReader();

                    if (dr.HasRows)
                    {
                        leido = true;
                        while (dr.Read())
                        {
                            EN_Anuncio anuncio = new EN_Anuncio();
                            anuncio.ENPropiedad = new EN_Propiedad();

                            anuncio.Precio = float.Parse(dr["precio"].ToString());
                            anuncio.Desc = dr["desc"].ToString();
                            anuncio.Id = int.Parse(dr["id"].ToString());
                            anuncio.ENPropiedad.Metros = int.Parse(dr["metros"].ToString());
                            anuncio.ENPropiedad.Cod_postal = dr["cod_postal"].ToString();
                            anuncio.ENPropiedad.Img1 = dr["img1"].ToString();
                            anuncio.ENPropiedad.Img2 = dr["img1"].ToString();
                            anuncio.ENPropiedad.Img3 = dr["img1"].ToString();
                            en.lista.Add(anuncio);
                        }
                        en.lista = en.lista.OrderBy(x => x.Id).ToList();
                        en.desde = en.lista[0].Id.ToString();
                        en.hasta = en.lista[en.lista.Count - 1].Id.ToString();
                    }
                }
                catch (SqlException e)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception en readFavoritos() de CAD_Favorito: {e.Message}");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception en readFavoritos() de CAD_Favorito: {e.Message}");
                }
            }
            return leido;
        }

        /// <summary>
        /// Metodo  createFavoritos que crea un favorito nuevo.
        /// </summary>
        /// <param name="en">Parametro del tipo EN_Favoritos.</param>
        /// <returns>Devuelve true si el favorito de ha creado de forma correcta.</returns>
        public bool createFavoritos(EN_Favoritos en)
        {
            string query = "INSERT INTO [dbo].[Favorito] (usuario, anuncio) VALUES ('" + en.usuario_id + "',  " + en.anuncio_id + ")";

            bool creado = false;
            
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
                    System.Diagnostics.Debug.WriteLine($"Exception en createFavoritos() de CAD_Favoritos: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception en createFavoritos() de CAD_Favoritos: {e.Message}");
                }
            }

            return creado;
        }

        /// <summary>
        /// Metodo deleteFavoritos que borra un favorito ya existente.
        /// </summary>
        /// <param name="en">Parametro del tipo EN_Favoritos.</param>
        /// <returns>Devuelve true si se ha borrado el favorito de forma correcta.</returns>
        public bool deleteFavoritos(EN_Favoritos en)
        {
            SqlConnection conn = null;
            bool borrado = false;
            string query = "DELETE FROM [dbo].[Favorito] WHERE anuncio = '" + en.anuncio_id + "' and usuario = '" + en.usuario_id + "'";
            
            try
            {
                try
                {
                    conn = new SqlConnection(constring);
                    conn.Open();
                    SqlCommand delete = new SqlCommand(query, conn);
                    delete.ExecuteNonQuery();
                    borrado = true;
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
            }
            catch (System.Threading.ThreadAbortException) { }
            

            return borrado;
        }

        /// <summary>
        /// Metodo updateFavoritos que actualiza un favorito ya existente.
        /// </summary>
        /// <param name="en">Parametro del tipo EN_Favoritos.</param>
        /// <returns>Devuelve true si el favorito se ha actualizado de forma correcta.</returns>
        public bool updateFavoritos(EN_Favoritos en)
        {
            bool actualizado = true;
            string query = "UPDATE [dbo].[Favorito] SET anuncio= " + en.anuncio_id + "WHERE usuario ='" + en.usuario_id + "'";

            try
            {
                SqlConnection conex = null;
                conex = new SqlConnection(constring);
                conex.Open();
                SqlCommand c = new SqlCommand(query, conex);
                c.ExecuteNonQuery();
                conex.Close();
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                actualizado = false;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                actualizado = false;
            }
            return actualizado;
        }

        public bool leerFavorito(EN_Favoritos en)
        {
            string query = "Select * from [dbo].[Favorito] where usuario ='" + en.usuario_id + "' and anuncio ='" + en.anuncio_id + "'";
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
                        return true;
                    }
                }
                catch (SqlException e)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception en existeUsuario() de CAD_Usuario: {e.Message}");
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Exception en existeUsuario() de CAD_Usuario: {e.Message}");
                }

            }

            return existe;
        }
    }
}
