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

namespace library.CAD
{
    /// <summary>
    /// Clase CAD_Comentarios.
    /// </summary>
    public class CAD_Comentarios
    {
        private string constring;

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        public CAD_Comentarios()
        {
            constring = ConfigurationManager.ConnectionStrings["InmoData"].ToString();
        }

        /// <summary>
        /// Metodo createComentario que crea un comentario nuevo.
        /// </summary>
        /// <param name="en">Parametro del tipo EN_Comentarios.</param>
        /// <returns>Devuelve true si se ha creado el comentario.</returns>
        public bool createComentario(EN_Comentarios en)
        {
            string query = "Insert INTO [dbo].[Comentario] (texto, usuario, anuncio) VALUES ('" + en.texto + "', '" + en.usuario + "', " + en.anuncio + ")";
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
        /// Metodo deleteComentario que borra un comentario ya existente.
        /// </summary>
        /// <param name="en">Parametro del tipo EN_Comentarios.</param>
        /// <returns>Devuelve true si el comentario se ha borrado correctamente.</returns>
        public bool deleteComentario(EN_Comentarios en)
        {
            bool borrado = false;
            string query = "DELETE FROM [dbo].[Comentario] WHERE id = " + en.id;

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

        /// <summary>
        /// Metodo readComentario que lee un comentario concreto.
        /// </summary>
        /// <param name="en">Parametro del tipo EN_Comentarios.</param>
        /// <returns>Devuelve true si el commentario que queremos se ha leido de forma correcta.</returns>
        public bool readComentario(EN_Comentarios en)
        {
            bool leido = false;
            string query = "SELECT texto, usuario FROM comentario WHERE anuncio = " + en.anuncio + ";";

            using (SqlConnection connection = new SqlConnection(constring))
            {
                try
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand(query, connection);
                    SqlDataReader dr = com.ExecuteReader();
                    if (dr.HasRows)
                    {
                        System.Diagnostics.Debug.WriteLine("gola2");
                        while (dr.Read())
                        {
                            leido = true;
                            EN_Comentarios aux = new EN_Comentarios();
                            aux.texto = dr["texto"].ToString();
                            aux.usuario = dr["usuario"].ToString();
                            en.lista.Add(aux);
                        }
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
        /// Metodo readFirstComentario que lee el primer comentario.
        /// </summary>
        /// <param name="en">Parametro del tipo EN_Comentarios</param>
        /// <returns>Devuelve true si el primer comentario se ha leido.</returns>
        public bool readFirstComentario(EN_Comentarios en)
        {
            bool first = false;
            string query = "Select * From [dbo].[Comentario]";
            try
            {
                SqlConnection conex = null;
                conex = new SqlConnection(constring);
                conex.Open();
                SqlCommand c = new SqlCommand(query, conex);
                SqlDataReader reader = c.ExecuteReader();
                reader.Read();

                en.id = int.Parse(reader["id"].ToString());
                en.texto = reader["texto"].ToString();
                en.usuario = reader["usuario"].ToString();
                en.anuncio = int.Parse(reader["anuncio"].ToString());

                first = true;
                reader.Close();
                conex.Close();
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                first = false;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                first = false;
            }
            return first;
        }

        /// <summary>
        /// Metodo readNextComentario que lee el siguiente comentario al actual.
        /// </summary>
        /// <param name="en">Parametro del tipo EN_Comentarios.</param>
        /// <returns>Devuelve true si el siguiente comentario se ha leido correctamente.</returns>
        public bool readNextComentario(EN_Comentarios en)
        {
            bool encontrado = false;
            bool leerSiguiente = true;
            string query = "Select * From [dbo].[Comentario]";

            try
            {
                SqlConnection conex = null;
                conex = new SqlConnection(constring);
                conex.Open();
                SqlCommand c = new SqlCommand(query, conex);
                SqlDataReader reader = c.ExecuteReader();

                while (reader.Read())
                {
                    if (encontrado)
                    {
                        en.id = int.Parse(reader["id"].ToString());
                        en.texto = reader["texto"].ToString();
                        en.usuario = reader["usuario"].ToString();
                        en.anuncio = int.Parse(reader["anuncio"].ToString());
                        break;
                    }
                    else if (en.id == int.Parse(reader["id"].ToString()))
                    {
                        encontrado = true;
                    }
                };
                leerSiguiente = true;
                reader.Close();
                conex.Close();
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                leerSiguiente = false;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                leerSiguiente = false;
            }
            return leerSiguiente;
        }

        /// <summary>
        /// Metodo readPrevComentario que lee el comentario anterior al actual.
        /// </summary>
        /// <param name="en">Parametro del tipo EN_Comentarios.</param>
        /// <returns>Devuelve true si el comentrio anterior se ha leido correctamente.</returns>
        public bool readPrevComentario(EN_Comentarios en)
        {
            bool leerAnterior = false;
            string query = "Select * From [dbo].[Comentario]";

            try
            {
                SqlConnection conex = null;
                conex = new SqlConnection(constring);
                conex.Open();
                SqlCommand c = new SqlCommand(query, conex);
                SqlDataReader reader = c.ExecuteReader();
                reader.Read();

                EN_Comentarios comentAux = new EN_Comentarios();
                /*comentAux.id = reader["id"].ToString();
                comentAux.texto = reader["texto"].ToString();
                comentAux.usuario = reader["usuario"].ToString();
                comentAux.anuncio = reader["anuncio"].ToString();*/

                while (reader.Read())
                {
                    if (int.Parse(reader["id"].ToString()) == en.id)
                    {
                        leerAnterior = true;
                        break;
                    }
                    comentAux.id = int.Parse(reader["id"].ToString());
                    comentAux.texto = reader["texto"].ToString();
                    comentAux.usuario = reader["usuario"].ToString();
                    comentAux.anuncio = int.Parse(reader["anuncio"].ToString());
                }
                en.id = comentAux.id; 
                en.texto = comentAux.texto;
                en.usuario = comentAux.usuario;
                en.anuncio = comentAux.anuncio;

                reader.Close();
                conex.Close();
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                leerAnterior = false;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("User operation has failed. Error: {0}", e.Message);
                leerAnterior = false;
            }
            return leerAnterior;
        }

        /// <summary>
        /// Metodo updateComentario que actualiza un comentario ya existente.
        /// </summary>
        /// <param name="en">Parametro del tipo EN_Comentarios.</param>
        /// <param name="texto">Parametro del tipo string que contiene la cadena del nuevo comentario.</param>
        /// <returns>Devuelve true si el comentario se ha actualizado correctamente.</returns>
        public bool updateComentario(EN_Comentarios en, string texto)
        {
            bool actualizado = true;
            string query = "UPDATE [dbo].[Comentario] SET texto='" + texto + "' WHERE id = " + en.id;

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
    }
}
