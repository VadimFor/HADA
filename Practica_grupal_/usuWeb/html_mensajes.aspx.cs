using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library.EN;

namespace usuWeb
{
    public partial class html_mensajes : System.Web.UI.Page
    {
        /// <summary>
        /// Entidad de negocio para un Mensaje.
        /// </summary>
        EN_Mensaje en = new EN_Mensaje();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("html_IniciarSesion.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EN_Usuario en = (EN_Usuario)Session["user"];
                ViewState["user"] = en.email;
                SqlDataSource1.SelectParameters["usu_iniciado"].DefaultValue = ViewState["user"].ToString();
                SqlDataSource2.SelectParameters["usu_iniciado"].DefaultValue = ViewState["user"].ToString();
                ElegirConversacion.DataBind();
                if (ElegirConversacion.Items.Count == 1)
                { // Si solo hay un item en el DropDownList, el usuario no tiene ninguna conversación.
                    EntradaMensaje.Enabled = false;
                }
                else
                {
                    EntradaMensaje.Enabled = true;
                }
            }
        }
        /// <summary>
        /// Pobla la lista de mensajes en el repeater.
        /// </summary>
        protected void BindRepeater()
        {
            ListaMensajes.DataBind();
        }
        /// <summary>
        /// Manejador del evento en el cual un usuario haga clic en el botón 'Enviar Mensaje'.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EnviarMensaje(object sender, EventArgs e)
        {
            if (ElegirConversacion.Items.Count != 1)
            {
                en.Texto = EntradaMensaje.Text;
                en.Usuario_emi = ViewState["user"].ToString();
                en.Usuario_recep = ElegirConversacion.SelectedValue;
                en.createMensaje();
                BindRepeater();
                EntradaMensaje.Text = "";
            }
            else
            {
                AvisoPanel.Visible = true;
            }
        }
        /// <summary>
        /// Manejador del evento en el cual un usuario cambie el item elegido en el DropDownList.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ElegirConversacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRepeater();
        }
    }
}