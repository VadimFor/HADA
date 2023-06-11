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
    public partial class html_propiedad : System.Web.UI.Page
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


        EN_Categoria enC = new EN_Categoria();
        EN_Propiedad en = new EN_Propiedad();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable d = new DataTable();
            d = en.mostrarPropiedades();
            ListView1.DataSource = d;
            ListView1.DataBind();
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if ((EN_Usuario)Session["user"] != null)
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    LinkButton lkbtn = e.Item.FindControl("favs") as LinkButton;
                    lkbtn.Visible = true;
                    Literal lblfavs = e.Item.FindControl("lbl_favs") as Literal;
                    lblfavs.Text = "Añadir a favoritos";
                }
            }
            else
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    LinkButton lkbtn = e.Item.FindControl("favs") as LinkButton;
                    lkbtn.Visible = false;
                    Literal lblfavs = e.Item.FindControl("lbl_favs") as Literal;
                    lblfavs.Text = "";
                }
            }
        }

        protected void elegirCat(object sender, EventArgs e)
        {
            DataTable d = new DataTable();
            string categoria = DropDownList1.SelectedValue;

            if (categoria != "Todas")
            {
                ListView1.DataSource = null;
                ListView1.DataBind();
                d = en.filtrarCategorias(categoria);
                ListView1.DataSource = d;
                ListView1.DataBind();
            }
            else
            {
                Page_Load(sender, e);
            }
        }

        protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            // set current page startindex,max rows and rebind to false  
            DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            // Rebind the ListView1  
            ListView1.DataBind();
        }

        protected void anyadirFavoritos(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                EN_Propiedad en = new EN_Propiedad();
                EN_Anuncio enA = new EN_Anuncio();
                EN_Favoritos enF = new EN_Favoritos();

                LinkButton source = (LinkButton)sender;
                enA.Propiedad = Int32.Parse(source.CommandArgument);

                if (enA.leerPorPropiedad())
                {
                    enF.anuncio_id = enA.Id;
                }

                EN_Usuario usu = (EN_Usuario)Session["user"];
                enF.usuario_id = usu.email;

                enF.createFavoritos();
            }
        }
        /// <summary>
        /// Método para sacar el identificador del anuncio correspondiente a una propiedad y redirigir el usuario a la página del anuncio.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void verAnuncio(object sender, EventArgs e)
        {
            LinkButton source = (LinkButton)sender;
            EN_Anuncio en = new EN_Anuncio();
            en.Propiedad = Int32.Parse(source.CommandArgument);
            en.leerPorPropiedad();
            Response.Redirect("html_anuncio.aspx?id=" + en.Id);
        }
    }
}