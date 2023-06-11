using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using library.EN;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Web;

namespace library.CAD
{
    public class CAD_Usuario
    {

        private string constring;
        public CAD_Usuario() { constring = ConfigurationManager.ConnectionStrings["InmoData"].ToString(); }

        private string hashkey = "*hg849gh84th==3tg7-534d=_";

        private static string nombreDB = "InmoData";

        /// <summary>
        /// Método que se usa al iniciar sesión
        /// Método que comprueba si el usuario existe: busca en la base de datos si hay algún usuario con ese email & contraseña.
        /// </summary>
        /// <returns>devuelve true si el email & contraseña coinciden con alguna fila de la base de datos.</returns>
        public bool existeUsuario(EN_Usuario en)
        {
            string query = "Select * from [dbo].[Usuario] where email='" + en.email + "' and clave ='" + en.clave + "' and verificado = 1"; bool existe = false;

            try
            {
                //abro conexión y mando query
                SqlConnection connection = new SqlConnection(constring);
                connection.Open();
                SqlCommand com = new SqlCommand(query, connection);

                //truco para calcular el número de filas con los datos entregados
                SqlDataReader r_aux = com.ExecuteReader();
                DataTable _aux = new DataTable(); _aux.Load(r_aux); int count = _aux.Rows.Count;
                r_aux.Close();

                if(count > 0){
                    
                    existe = true; // si hay algún usuario que coincide con este email & contraseña entonces los datos son correctos

                    SqlDataReader dr = com.ExecuteReader();

                    dr.Read();
                    en.nombre = dr["nombre"].ToString();
                    en.telefono = dr["telefono"].ToString();
                    en.esAdmin = dr.GetBoolean(dr.GetOrdinal("esAdmin"));
                    //email y contraseña ya están puestas en el textbox al iniciar sesión
                    dr.Close();
                }
                connection.Close();

            }
            catch (SqlException e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }
            catch (Exception e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }

            return existe;
        }




        /// <summary>
        /// Método que se usa al registrarse
        /// Crea al usuario en la base de datos con los datos introducidos.
        /// </summary>
        /// <returns>devuelve true si se ha podido crear correctamente.</returns>
        public bool crearUsuario(EN_Usuario en)
        {
            string hash = CreateSHAHash(Guid.NewGuid().ToString(), hashkey);
            hash = hash.Replace('+', 't');
            string query = $"Insert into [dbo].[Usuario] (nombre,apellido, nif, email, telefono, clave, esAdmin, verificado, hashVerificacion) values ('{en.nombre}','{en.apellido}','{en.nif}','{en.email}','{en.telefono}','{en.clave}','{en.esAdmin}',0,'{hash}')";
            bool creado = false;

            try
            {
                //abro conexión y mando query
                SqlConnection connection = new SqlConnection(constring);
                connection.Open();
                SqlCommand com = new SqlCommand(query, connection);
                com.ExecuteNonQuery();

                connection.Close();

                creado = true;
                enviarMensajeVerificacion(en.email, hash);
            }
            catch (SqlException e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }
            catch (Exception e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }

            return creado;
        }

        /// <summary>
        /// Método que se usa en el panel de usuario.
        /// Borrar al usuario de la base de datos.
        /// </summary>
        /// <returns>devuelve true si se ha podido borrar correctamente.</returns>
        public bool borrarUsuario(EN_Usuario en)
        {
            string query = $"Delete from [dbo].[Usuario] where email = '{en.email}'";
            bool borrado = false;

            try
            {
                //abro conexión y mando query
                SqlConnection connection = new SqlConnection(constring);
                connection.Open();
                SqlCommand com = new SqlCommand(query, connection);
                com.ExecuteNonQuery();

                connection.Close();

                borrado = true;

            }
            catch (SqlException e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }
            catch (Exception e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }

            return borrado;
        }

        /// <summary>
        /// Método que se usa en el panel de usuario.
        /// Actualiza la información de usuario con los datos que no son vacios dentro del en
        /// </summary>
        /// <returns>devuelve true si se ha podido actualizar correctamente.</returns>
        public bool actualizarUsuario(EN_Usuario en)
        {
            bool actualizado = false;

            string query = $"Update [dbo].[Usuario] set nombre=";
            if (en.clave != "")
            {
                // FALTA HACER /////////////////////////////////////////////////////////////
            }

            try
            {
                //abro conexión y mando query
                SqlConnection connection = new SqlConnection(constring);
                connection.Open();
                SqlCommand com = new SqlCommand(query, connection);
                com.ExecuteNonQuery();

                connection.Close();

                actualizado = true;

            }
            catch (SqlException e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }
            catch (Exception e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }

            return actualizado;
        }

        public DataSet admin_getUsuarios()
        {
            DataSet dataset = new DataSet();
            string query = "Select * from [dbo].[Usuario]";

            try
            {
                //OBTENGO LOS DATOS DE LA BASE DE DATOS
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[nombreDB].ToString());
                SqlDataAdapter sqladaprter = new SqlDataAdapter(query, con);     
                sqladaprter.Fill(dataset, "Usuarios"); //relllena el dataset con el resultado del sqladapter

            }
            catch (SqlException e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }
            catch (Exception e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }

            return dataset;
        }

        public bool admin_modUsuarios(DataSet dataset, SqlCommand updateCommand)
        {
            bool modificados = false;
            try
            {
                //OBTENGO LOS DATOS DE LA BASE DE DATOS
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InmoData"].ToString());
                string query = "Select * from [dbo].[Usuario]";
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, con);

                ////// UPDATE
                string updateQuery = "Update [dbo].[Usuario] set nombre = @nombre," +
                                                         "apellido = @apellido," +
                                                         "nif = @nif," +
                                                         "telefono = @telefono," +
                                                         "clave = @clave" +
                                                         " where email = @email";
                updateCommand.CommandText = updateQuery;
                updateCommand.Connection = con;

                sqlAdapter.UpdateCommand = updateCommand;

                sqlAdapter.Update(dataset, "Usuarios");

                modificados = true;
            }
            catch (SqlException e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }
            catch (Exception e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }

            return modificados;
        }

        public bool admin_deleteUsuario(DataSet dataset, SqlCommand deleteCommand)
        {
            bool borrado = false;
            try
            {
                //OBTENGO LOS DATOS DE LA BASE DE DATOS
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["InmoData"].ToString());
                string query = "Select * from [dbo].[Usuario]";
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, con);

                string deletequery = "Delete from [dbo].[Usuario] where email = @email";

                deleteCommand.CommandText = deletequery;
                deleteCommand.Connection = con;

                sqlAdapter.DeleteCommand = deleteCommand;

                sqlAdapter.Update(dataset, "Usuarios");

                borrado = true;

            }
            catch (SqlException e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }
            catch (Exception e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }

            return borrado;
        }

        /// <summary>
        /// Método que se usa para mostrar información del que ha iniciado sesion en perfil de usuario
        /// </summary>
        /// <returns>devuelve true si ha encontrado al usuario</returns>
        public bool perfilUsuario_getUsuario(EN_Usuario en)
        {
            string query = "Select * from Usuario where email='" + en.email + "'";
            bool existe = false;

            try
            {
                //abro conexión y mando query
                SqlConnection connection = new SqlConnection(constring);
                connection.Open();
                SqlCommand com = new SqlCommand(query, connection);

                //truco para calcular el número de filas con los datos entregados
                SqlDataReader r_aux = com.ExecuteReader();
                DataTable _aux = new DataTable(); _aux.Load(r_aux); int count = _aux.Rows.Count;
                r_aux.Close();

                if (count > 0)
                {
                    existe = true; // si hay algún usuario que coincide con este email entonces los datos son correctos

                    SqlDataReader dr = com.ExecuteReader();

                    dr.Read();
                    en.nombre = dr["nombre"].ToString();
                    en.apellido = dr["apellido"].ToString(); 
                    en.nif = dr["nif"].ToString();
                    en.telefono = dr["telefono"].ToString();
                    //en.email = dr["email"].ToString();

                    dr.Close();
                }
                connection.Close();

            }
            catch (SqlException e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }
            catch (Exception e) { Console.WriteLine($"Exception de CAD_Usuario: {e.Message}"); }

            return existe;
        }

        public static string CreateSHAHash(string Text, string Salt)
        {
            System.Security.Cryptography.SHA512Managed HashTool = new System.Security.Cryptography.SHA512Managed();
            Byte[] HashAsByte = System.Text.Encoding.UTF8.GetBytes(string.Concat(Text, Salt));
            Byte[] EncryptedBytes = HashTool.ComputeHash(HashAsByte);
            HashTool.Clear();
            return Convert.ToBase64String(EncryptedBytes);
        }

        protected void enviarMensajeVerificacion(string email, string hash)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            MailMessage message = new MailMessage();

            try
            {
                MailAddress fromAddress = new MailAddress("sanvihouses@gmail.com");
                MailAddress toAddress = new MailAddress(email);

                message.From = fromAddress;
                message.To.Add(toAddress);
                message.Subject = "Verificación de cuenta Sanvi Houses";
                string domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                message.Body = "Hola! Pincha en este enlace para verificar la cuenta: " + domainName + "/html_verification.aspx?correo=" + email + "&hash=" + HttpUtility.UrlEncode(hash);
                message.IsBodyHtml = true;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential("sanvihouses@gmail.com", "hola123_");
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("User operation has failed.Error: {0}", ex.Message);
            }

        }

        public bool verificarUsuario(EN_Usuario en)
        {
            string query = "Select * from [dbo].[Usuario] where email='" + en.email + "' and hashVerificacion = '" + en.hash + "'";
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
                        dr.Close();
                        query = "Update Usuario set verificado = 1 where email='" + en.email + "'";
                        SqlCommand comm = new SqlCommand(query, connection);
                        comm.ExecuteNonQuery();
                        en.verificado = true;
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
