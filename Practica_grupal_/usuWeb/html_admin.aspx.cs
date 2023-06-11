using library.EN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace usuWeb
{
    public partial class html_verUsuarios : System.Web.UI.Page
    {
        protected EN_Administrador a = new EN_Administrador();

        private static bool mostrandoUsuarios = true;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            EN_Usuario en = new EN_Usuario();

            if ((EN_Usuario)Session["user"] != null)
            {
                en = (EN_Usuario)Session["user"];
                if (!en.esAdmin)
                {
                    Response.Redirect("html_404.aspx");
                }
            }
            else
            {
                Response.Redirect("html_iniciarSesion.aspx");
            }
        }
        
        protected void Page_PreRender(object sender, EventArgs e)
        {
            cargarEstadisticas();
            int totalDias = a.noche + a.mañana + a.tarde;
            float porcentajeMañana = (a.mañana * 100) / totalDias;
            float porcentajeNoche = (a.noche * 100) / totalDias;
            float porcentajeTarde = (a.tarde * 100) / totalDias;
        } 
        protected void Page_Load(object sender, EventArgs e)
        {
            //POR SEGURIDAD QUE NO SE PUEDA ENTRAR CON EL SIMPLE LINK
            if ((EN_Usuario)Session["user"] != null) //si esta logeado
            {
                EN_Usuario usuario = (EN_Usuario)Session["user"];

                if (usuario.perfilUsuario_getUsuario(usuario))
                {
                    if (!usuario.esAdmin) //si esta logeado y si es admin
                    {
                        Response.Redirect("html_IniciarSesion.aspx");
                    }
                    else
                    {
                        if (!Page.IsPostBack)
                        {
                            GetUsersFromDB();
                            mostrandoUsuarios = true;
                        }
                        else
                        {
                            if (mostrandoUsuarios)
                            {
                                GetUsersFromCache();
                            }
                            else
                            {
                                GetPropiedadesFromCache();
                            }
                        }
                    }                 
                }
            }
            else //si no esta logeado
            {
                Response.Redirect("html_IniciarSesion.aspx");
            }
        }

        private void GetUsersFromDB()
        {
            EN_Usuario u = new EN_Usuario();
            DataSet dataset = u.admin_getUsuarios();

            dataset.Tables["Usuarios"].PrimaryKey = new DataColumn[] { dataset.Tables["Usuarios"].Columns["email"] };
            Cache.Insert("usersCache", dataset, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);

            GridView1.DataSource = dataset;
            GridView1.DataBind();
            mostrandoUsuarios = true;
        }

        /// <summary>
        /// Método para sacar las propiedades de la cache
        /// </summary>
        private void GetPropiedadesFromDB()
        {
            EN_Propiedad u = new EN_Propiedad();
            DataSet dataset = u.admin_getPropiedades();

            dataset.Tables["Propiedad"].PrimaryKey = new DataColumn[] { dataset.Tables["Propiedad"].Columns["id"] };
            Cache.Insert("propiedadesCache", dataset, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);

            GridView1.DataSource = dataset;
            GridView1.DataBind();
            mostrandoUsuarios = false;
        }

        
        /// <summary>
        /// Método para sacar los usuarios de la cache
        /// </summary>
        private void GetUsersFromCache()
        {
            if(Cache["usersCache"] != null)
            {
                DataSet dataset = (DataSet)Cache["usersCache"];

                GridView1.DataSource = dataset;
                GridView1.DataBind();
                mostrandoUsuarios = true;
            }
        }

        /// <summary>
        /// Método para sacar las propiedades de la cache
        /// </summary>
        private void GetPropiedadesFromCache()
        {
            if (Cache["propiedadesCache"] != null)
            {
                DataSet dataset = (DataSet)Cache["propiedadesCache"];

                GridView1.DataSource = dataset;
                GridView1.DataBind();
                mostrandoUsuarios = false;
                
            }
        }

        /// <summary>
        /// Método para mostrar los usuarios la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mostrarUsuarios_click(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1; //quitamos la fila de edición
            GetUsersFromDB();
        }


        /// <summary>
        /// Método para mostrar las propiedadesde la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void mostrarPropiedades_click(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1; //quitamos la fila de edición
            GetPropiedadesFromDB();
        }

        /// <summary>
        /// Método para guardar los cambios en la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void guardarCambios_click(object sender, EventArgs e)
        {
            if (mostrandoUsuarios)
            {
                DataSet dataset = (DataSet)Cache["usersCache"];
                EN_Usuario u = new EN_Usuario();

                //UPDATE
                SqlCommand updateCommand = new SqlCommand();
                updateCommand.Parameters.Add("@email", SqlDbType.NVarChar, 50, "email");
                updateCommand.Parameters.Add("@nombre", SqlDbType.NVarChar, 50, "nombre");
                updateCommand.Parameters.Add("@apellido", SqlDbType.NVarChar, 50, "apellido");
                updateCommand.Parameters.Add("@nif", SqlDbType.NVarChar, 50, "nif");
                updateCommand.Parameters.Add("@telefono", SqlDbType.NVarChar, 50, "telefono");
                updateCommand.Parameters.Add("@clave", SqlDbType.NVarChar, 50, "clave");       
                u.admin_modUsuarios(dataset, updateCommand);

                ////// DELETE
                SqlCommand deleteCommand = new SqlCommand();
                deleteCommand.Parameters.Add("@email", SqlDbType.NVarChar, 50, "email");
                u.admin_deleteUsuario(dataset, deleteCommand);
            }
            else
            {
                DataSet dataset = (DataSet)Cache["propiedadesCache"];
                EN_Propiedad u = new EN_Propiedad();

                SqlCommand updateCommand = new SqlCommand();
                updateCommand.Parameters.Add("@id", SqlDbType.Int).SourceColumn = "id";
                updateCommand.Parameters.Add("@metros", SqlDbType.Int).SourceColumn = "metros";
                updateCommand.Parameters.Add("@cod_postal", SqlDbType.NVarChar, 8, "cod_postal");
                updateCommand.Parameters.Add("@usuario", SqlDbType.NVarChar, 50, "usuario");
                updateCommand.Parameters.Add("@categoria", SqlDbType.NVarChar, 32, "categoria");
                updateCommand.Parameters.Add("@direccion", SqlDbType.NVarChar, 50, "direccion");
                updateCommand.Parameters.Add("@img1", SqlDbType.NVarChar, 150, "img1");
                updateCommand.Parameters.Add("@img2", SqlDbType.NVarChar, 150, "img2");
                updateCommand.Parameters.Add("@img3", SqlDbType.NVarChar, 150, "img3");
                u.admin_modPropiedades(dataset, updateCommand);

                ////// DELETE
                SqlCommand deleteCommand = new SqlCommand();
                deleteCommand.Parameters.Add("@id", SqlDbType.Int).SourceColumn = "id";
                u.admin_deletePropiedades(dataset, deleteCommand);

            }
        }

        /// <summary>
        /// Método para editar la fila
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex; //ponemos al fila en estado de edicion

            if (mostrandoUsuarios) { GetUsersFromCache(); }
            else { GetPropiedadesFromCache(); }
        }

        /// <summary>
        /// Método para actualizar lo editado de la fila
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (mostrandoUsuarios)
            {
                if (Cache["usersCache"] != null)
                {
                    DataSet dataset = (DataSet)Cache["usersCache"]; //obtengo la tabla del cache
                    DataRowCollection listaFilas = dataset.Tables["Usuarios"].Rows;
                    DataRow fila = listaFilas[e.RowIndex];

                    //fila["email"] = e.NewValues["email"];
                    fila["nombre"] = e.NewValues["nombre"];
                    fila["apellido"] = e.NewValues["apellido"];
                    fila["nif"] = e.NewValues["nif"];
                    fila["telefono"] = e.NewValues["telefono"];
                    fila["clave"] = e.NewValues["clave"];

                    //Cache["UsersData"] = dataset;
                    Cache.Insert("UsersData", dataset, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                    GridView1.EditIndex = -1; //quitamos la fila de edición
                    GetUsersFromCache();
                }
            }
            else
            {
                if (Cache["propiedadesCache"] != null)
                {
                    
                    DataSet dataset = (DataSet)Cache["propiedadesCache"]; //obtengo la tabla del cache
                    DataRowCollection listaFilas = dataset.Tables["Propiedad"].Rows;
                    DataRow fila = listaFilas[e.RowIndex];

                    //fila["id"] = e.NewValues["id"];
                    fila["metros"] = e.NewValues["metros"];
                    fila["cod_postal"] = e.NewValues["cod_postal"];
                    fila["usuario"] = e.NewValues["usuario"];
                    fila["categoria"] = e.NewValues["categoria"];
                    fila["direccion"] = e.NewValues["direccion"];
                    fila["img1"] = e.NewValues["img1"];
                    fila["img2"] = e.NewValues["img2"];
                    fila["img3"] = e.NewValues["img3"];

                    //Cache["propiedadesCache"] = dataset;
                    Cache.Insert("propiedadesCache", dataset, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                    GridView1.EditIndex = -1; //quitamos la fila de edición
                    GetPropiedadesFromCache();       
                }
            }
        }

        /// <summary>
        /// Método para cancelar la edición de la fila
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1; //quitamos la fila de edición
            if (mostrandoUsuarios) { GetUsersFromCache(); }
            else { GetPropiedadesFromCache(); }
        }

        /// <summary>
        /// Método para eliminar fila
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (mostrandoUsuarios) {
                if (Cache["usersCache"] != null)
                {
                    DataSet dataset = (DataSet)Cache["usersCache"]; //obtengo la tabla del cache
                    DataRowCollection listaFilas = dataset.Tables["Usuarios"].Rows;
                    DataRow fila = listaFilas[e.RowIndex];
                    fila.Delete();

                    Cache.Insert("usersCache", dataset, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                    GetUsersFromCache();
                }
            }
            else
            {
                if (Cache["propiedadesCache"] != null)
                {
                    DataSet dataset = (DataSet)Cache["propiedadesCache"]; //obtengo la tabla del cache
                    DataRowCollection listaFilas = dataset.Tables["Propiedad"].Rows;
                    DataRow fila = listaFilas[e.RowIndex];
                    fila.Delete();

                    Cache.Insert("propiedades", dataset, null, DateTime.Now.AddHours(24), System.Web.Caching.Cache.NoSlidingExpiration);
                    GetPropiedadesFromCache();
                }
            }
        }
        
        protected bool cargarEstadisticas()
        {
            if (a.recuperarEstadisticas())
            {
                int totalDias = a.noche + a.mañana + a.tarde;
                float porcentajeMañana = (a.mañana * 100) / totalDias;
                float porcentajeNoche = (a.noche * 100) / totalDias;
                float porcentajeTarde = (a.tarde * 100) / totalDias;
                string script = @"cargarEstadisticas('" + porcentajeMañana + "', '" + porcentajeTarde + "', '" + porcentajeNoche + "')";
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", script, true);
                return true;
            }
            else
            {
                return false;
            }
        } 
    }
}