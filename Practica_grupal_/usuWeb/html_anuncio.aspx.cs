using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library.EN;

namespace usuWeb
{
    public partial class html_anuncio : System.Web.UI.Page
    {
        /// <summary>
        /// Entidad de negocio para un Anuncio.
        /// </summary>
        EN_Anuncio en = new EN_Anuncio();
        /// <summary>
        /// 
        /// </summary>
        protected EN_Comentarios enComentario = new EN_Comentarios();
        /// <summary>
        /// Usuario iniciado de sesión.
        /// </summary>
        EN_Usuario usuarioIniciado = new EN_Usuario();
        /// <summary>
        /// Gestiona la página maestra que se utiliza al renderizar la página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("html_404.aspx");
            }
            if ((EN_Usuario)Session["user"] != null)
            {
                this.MasterPageFile = "~/Usuario.Master";
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            cargarComentarios();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Inicialización del listado de comentarios y los datos del anuncio.
            {
                en.Id = Int32.Parse(Request.QueryString["id"]);
                DataTable datos = new DataTable();
                datos = en.getDataFromAnuncio();
                DataRow dr = datos.Rows[0];
                en.Contacto = (string)dr["contacto"];
                MetrosLabel.Text = dr["metros"].ToString();
                DescriptionLabel.Text = dr["desc"].ToString().Replace(System.Environment.NewLine, "<br/>");
                CategoriaLabel.Text = dr["categoria"].ToString();
                DireccionLabel.Text = dr["direccion"].ToString();
                PropertyIdLabel.Text = dr["propiedad"].ToString();
                img1.Src = dr["img1"].ToString();
                img2.Src = "";
                img3.Src = "";
                if (dr["img2"].ToString() != null && dr["img2"].ToString() != "")
                    img2.Src = dr["img2"].ToString();
                if (dr["img3"].ToString() != null && dr["img3"].ToString() != "")
                    img3.Src = dr["img3"].ToString();

                ViewState["anuncioId"] = dr["id"];
                ViewState["anunciante"] = dr["usuario"];
                ViewState["contacto"] = dr["contacto"];

                if (dr.IsNull("fianza")) // Si el anuncio no tiene fianza, es una venta.
                {
                    PrecioLabel.Text = Convert.ToSingle(dr["precio"]).ToString();
                    EstadoLabel.Text = "VENTA";
                    FPLabel.Text = "Precio/metro²:";
                    VentaLabel.Visible = true;
                    VentaLabel.Text = dr["precio_por_m"].ToString();
                }
                else // Si tiene fianza, es un alquiler.
                {
                    PrecioLabel.Text = Convert.ToSingle(dr["precio"]).ToString() + " al mes";
                    EstadoLabel.Text = "ALQUILER";
                    FPLabel.Text = "Fianza:";
                    AlquilerLabel.Visible = true;
                    AlquilerLabel.Text = dr["fianza"].ToString();
                }
            }
            if (Session["user"] == null)
            {
                EnviarMensajeTextBox.Enabled = false; // Si no está iniciado de sesión un usuario, no se puede mandar mensajes ni publicar comentarios.
                EntradaComentario.Enabled = false;
            }
            else
            {
                EN_Usuario user = new EN_Usuario();
                user = (EN_Usuario)Session["user"];
                if (ViewState["anunciante"].ToString() == user.email)
                {
                    EnviarMensajeTextBox.Enabled = false; // Si el anunciante es el mismo usuario que está iniciado de sesión, no se puede mandar un mensaje a sí mismo.
                }
            }
        }
        /// <summary>
        /// Manejador de la publicación de un comentario.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PublicarComentario(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                var comentario = new EN_Comentarios();
                comentario.texto = EntradaComentario.Text;
                EN_Usuario user = new EN_Usuario();
                user = (EN_Usuario)Session["user"];
                comentario.usuario = user.email;
                comentario.anuncio = Int32.Parse(ViewState["anuncioId"].ToString());
                comentario.createComentario();
                EntradaComentario.Text = string.Empty;
            }
            else
            {
                Response.Redirect("html_IniciarSesion.aspx");
            }
        }
        /// <summary>
        /// Manejador del evento en el que un usuario haga click en el botón 'contactar usuario', el cual lleva el usuario al perfil del contacto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ContactButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("html_contacto.aspx?contacto=" + ViewState["contacto"].ToString());
        }
        /// <summary>
        /// Manejador del evento en el que un usuario haga click en el botón 'enviar mensaje', 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EnviarMensajeButton_Click(object sender, EventArgs e)
        {
            if(Session["user"] != null)
            {
                EN_Usuario user = new EN_Usuario();
                user = (EN_Usuario)Session["user"];

                if (EnviarMensajeTextBox.Text != "" && !(user.email == ViewState["anunciante"].ToString())) // No se puede mandar un mensaje a sí mismo.
                {
                    EN_Mensaje en = new EN_Mensaje();
                    en.Texto = EnviarMensajeTextBox.Text;
                    en.Usuario_emi = user.email;
                    en.Usuario_recep = ViewState["anunciante"].ToString();
                    if (en.createMensaje())
                    {
                        AvisoPanel.Visible = true;
                        EnviarMensajeConfirmar.Visible = true;
                    }
                }
                else
                {
                    if(EnviarMensajeTextBox.Text != "") { 
                    AvisoPanel.Visible = true;
                    AvisoSinTexto.Visible = true;
                    }
                    if(user.email == ViewState["anunciante"].ToString())
                    {
                        AvisoPanel.Visible = true;
                        AvisoSiMismo.Visible = true;
                    }
                }
            }
            else
            {
                AvisoPanel.Visible = true;
                AvisoSesion.Visible = true;
            }
        }
        protected void cargarComentarios()
        {
            int anuncioId = Int32.Parse(ViewState["anuncioId"].ToString());
            enComentario.anuncio = anuncioId;
            enComentario.readComentario();
        }

    }
}