using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library.EN;

namespace usuWeb
{
    public partial class html_verification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string correo = Request.QueryString["correo"] != null ? Request.QueryString["correo"].ToString() : "";
            string hash = Request.QueryString["hash"] != null ? Request.QueryString["hash"].ToString() : "";
            EN_Usuario user = new EN_Usuario();
            user.email = correo;
            user.hash = hash;
            if (user.verificarUsuario(user))
            {
                Aviso.Text = "Usuario verificado!";
            }
            else
            {
                Aviso.Text = "Usuario no verificado. :c";
            }
     
        }
    }
}