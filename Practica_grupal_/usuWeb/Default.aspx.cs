using library.EN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace usuWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if ((EN_Usuario)Session["user"] != null)
            {
                this.MasterPageFile = "/Usuario.Master";
            }
            else
            {
                this.MasterPageFile = "/Site1.Master";
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            EN_Propiedad en = new EN_Propiedad();
            DataTable d = new DataTable();
            d = en.mostrarPropiedadesDefault();
            ListView1.DataSource = d;
            ListView1.DataBind();
        }

        protected void verAnuncio(object sender, EventArgs e)
        {
            LinkButton source = (LinkButton)sender;

            Response.Redirect("html_anuncio.aspx?id=" + source.CommandArgument);
        }
    }
}