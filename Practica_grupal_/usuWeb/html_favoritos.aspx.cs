using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library.EN;

namespace usuWeb
{
    public partial class html_favoritos : System.Web.UI.Page
    {
        List<EN_Favoritos> ejemplos = new List<EN_Favoritos>();
        string limite = "3";
        string desde = "-1";
        string hasta = "-1";
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("html_IniciarSesion.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cargadoID.Text != "1")
            {
                EN_Favoritos en = new EN_Favoritos();
                en.limite = limite;
                en.desde = desde;
                en.hasta = hasta;
                en.usuario = (EN_Usuario)Session["user"];
                if (en.readFavoritos())
                {
                    ListView1.DataSource = en.lista;
                    ListView1.DataBind();
                }
                desdeID.Text = en.desde;
                hastaID.Text = en.hasta;
                cargadoID.Text = "1";
            }
        }

        protected void anteriorPag(object sender, EventArgs e)
        {
            EN_Favoritos en = new EN_Favoritos();
            en.volver = true;
            en.limite = limite;
            en.desde = desdeID.Text;
            en.hasta = hastaID.Text;
            en.usuario = (EN_Usuario)Session["user"];
            if (en.readFavoritos())
            {
                ListView1.DataSource = en.lista;
                ListView1.DataBind();
            }
            desdeID.Text = en.desde;
            hastaID.Text = en.hasta;
        }
        protected void siguientePag(object sender, EventArgs e)
        {
            EN_Favoritos en = new EN_Favoritos();
            en.avanzar = true;
            en.limite = limite;
            en.desde = desdeID.Text;
            en.hasta = hastaID.Text;
            en.usuario = (EN_Usuario)Session["user"];
            if (en.readFavoritos())
            {
                ListView1.DataSource = en.lista;
                ListView1.DataBind();
            }
            desdeID.Text = en.desde;
            hastaID.Text = en.hasta;
        }

        protected void verAnuncio(object sender, EventArgs e)
        {
            LinkButton source = (LinkButton)sender;

            Response.Redirect("html_anuncio.aspx?id="+ source.CommandArgument);

        }

        protected void eliminarFav(object sender, EventArgs e)
        {
            EN_Favoritos enF = new EN_Favoritos();

            LinkButton source = (LinkButton)sender;
            enF.anuncio_id = Int32.Parse(source.CommandArgument);
            
            EN_Usuario usu = (EN_Usuario)Session["user"];
            enF.usuario_id = usu.email;
            
            enF.deleteFavoritos();
            Response.Redirect("html_favoritos.aspx");
        }
    }
}