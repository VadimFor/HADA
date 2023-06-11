using library.EN;
using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace usuWeb
{
    public partial class html_perfilUsuario : System.Web.UI.Page
    {
        EN_Usuario usuarioIniciado = new EN_Usuario();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("html_IniciarSesion.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //a veces no cambia lo del navegador por el cache del navegador (por lo que no se ve
            //el cambio de imagen de perfil), con esto lo quito

            HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache, no-store, must-revalidate");
            HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            HttpContext.Current.Response.AddHeader("Expires", "0");

            if ((EN_Usuario)Session["user"] != null)
            {
                EN_Usuario usuario = new EN_Usuario();

                usuario = (EN_Usuario)Session["user"];

                if (usuario.perfilUsuario_getUsuario(usuario))
                {
                    usuario_nombre.Text = usuario.nombre;
                    usuario_apellido.Text = usuario.apellido;
                    usuario_nif.Text = usuario.nif;
                    usuario_telefono.Text = usuario.telefono;
                    usuario_email.Text = usuario.email;

                    usuarioIniciado = usuario;

                    //añado la foto del perfil si existe
                    if (File.Exists(Server.MapPath("/imagenes/" + usuario.email + ".jpg")))
                    {
                        foto_perfil.Src = "/imagenes/" + usuario.email + ".jpg";
                    }
                    else if (File.Exists(Server.MapPath("/imagenes/" + usuario.email + ".png")))
                    {
                        foto_perfil.Src = "/imagenes/" + usuario.email + ".png";
                    }
                    else if(File.Exists(Server.MapPath("/imagenes/" + usuario.email + ".jpeg")))
                    { // Si es un .jpeg
                        foto_perfil.Src = "/imagenes/" + usuario.email + ".jpeg";
                    }
                    else
                    {
                        foto_perfil.Src = "/imagenes/profile.jpg";
                    }
                }
            }
        }
        /// <summary>
        /// Método que permite a un usuario eliminar su propia cuenta.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void onBorrar(object sender, EventArgs e)
        {
            string usuarioIniciadoFoto = "/imagenes/" + usuarioIniciado.email;

            if (usuarioIniciado.borrarUsuario())
            {
                //elimino la foto del usuario
                if (File.Exists(Server.MapPath(usuarioIniciadoFoto + ".jpg")))
                {
                    File.Delete(Server.MapPath(usuarioIniciadoFoto + ".jpg"));
                }
                else if (File.Exists(Server.MapPath(usuarioIniciadoFoto + ".png")))
                {
                    File.Delete(Server.MapPath(usuarioIniciadoFoto + ".png"));
                }
                else
                { // Si es un .jpeg
                    File.Delete(Server.MapPath(usuarioIniciadoFoto + ".jpeg"));
                }
                Session.Abandon();
                Response.Redirect("default.aspx", false);
            }
        }
        /// <summary>
        /// Método para cambiar la imagen del perfil de un usuario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void onCambiarImagenPerfil(object sender, EventArgs e)
        {
            if (SubirImagenes.HasFiles)
            {
                try
                {
                    string usuarioIniciadoFoto = "/imagenes/" + usuarioIniciado.email;
                    bool imagencambiada = false;
                    foreach (HttpPostedFile uploadedFile in SubirImagenes.PostedFiles)
                    {
                        if (uploadedFile.ContentType == "image/png" || uploadedFile.ContentType == "image/jpg" || uploadedFile.ContentType == "image/jpeg")
                        {
                            //luego guardo la nueva foto con el mismo nombre que he eliminado
                            if (uploadedFile.ContentType == "image/png")
                            {
                                uploadedFile.SaveAs(Server.MapPath(usuarioIniciadoFoto + ".png"));
                                imagencambiada = true;
                                foto_perfil.Src = usuarioIniciadoFoto + ".png";
                            }
                            else if (uploadedFile.ContentType == "image/jpg")
                            {
                                uploadedFile.SaveAs(Server.MapPath(usuarioIniciadoFoto + ".jpg"));
                                imagencambiada = true;
                                foto_perfil.Src = usuarioIniciadoFoto + ".jpg";
                            }
                            else
                            { // Si es un .jpeg
                                uploadedFile.SaveAs(Server.MapPath(usuarioIniciadoFoto + ".jpeg"));
                                imagencambiada = true;
                                foto_perfil.Src = usuarioIniciadoFoto + ".jpeg";
                            }
                            listaArchivosSubidos.Text = uploadedFile.FileName;
                            if (!imagencambiada)
                            {
                                foto_perfil.Src = "/imagenes/profile.jpg";
                            }
                        }
                    }
                    //hago efectivo el cambio en el frontend
                    listaArchivosSubidos.Text = "";
                }
                catch { }

            }

        }
    }
}